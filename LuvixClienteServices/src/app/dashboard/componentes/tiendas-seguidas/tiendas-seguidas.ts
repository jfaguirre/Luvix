import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TiendaSeguidaDTO } from '../../../interface/tienda.models';

@Component({
  selector: 'app-tiendas-seguidas',
  imports: [],
  templateUrl: './tiendas-seguidas.html',
  styleUrl: './tiendas-seguidas.css'
})
export class TiendasSeguidas implements OnInit {

  tiendas: TiendaSeguidaDTO[] = [];
  loading = true;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.cargarTiendasSeguidas();
  }

  cargarTiendasSeguidas(): void {
    this.http.get<any>('http://localhost:5206/api/Tienda/tiendas-seguidas').subscribe({
      next: (response) => {
        this.tiendas = response.value || [];
        this.loading = false;
      },
      error: (err) => {
        console.error('Error al cargar tiendas seguidas', err);
        this.loading = false;
      }
    });
  }

  dejarDeSeguir(tienda: TiendaSeguidaDTO): void {
    const url = `http://localhost:5206/api/Seguidor/quitar/${tienda.id}`;
    this.http.post(url, {}).subscribe({
      next: () => {
        this.tiendas = this.tiendas.filter(t => t.id !== tienda.id);
      },
      error: (err) => {
        console.error('Error al dejar de seguir', err);
      }
    });
  }
}
