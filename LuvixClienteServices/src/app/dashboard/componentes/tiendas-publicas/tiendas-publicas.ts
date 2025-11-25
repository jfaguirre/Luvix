import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthService } from '../../../services/auth';
import { MostrarTiendaPublicaDTO } from '../../../interface/tienda.models';

@Component({
  selector: 'app-tiendas-publicas',
  imports: [],
  templateUrl: './tiendas-publicas.html',
  styleUrl: './tiendas-publicas.css'
})
export class TiendasPublicas {

  tiendas: MostrarTiendaPublicaDTO[] = [];
  loading = true;

  constructor(
    private http: HttpClient,
    public authService: AuthService
  ) {}

  ngOnInit(): void {
    this.cargarTiendas();
  }

  cargarTiendas(): void {
    this.http.get<any>('http://localhost:5206/api/Tienda/tiendas-publicas').subscribe({
      next: (response) => {
        this.tiendas = response.value || [];
        this.loading = false;
      },
      error: (err) => {
        console.error('Error al cargar tiendas', err);
        this.loading = false;
      }
    });
  }

  toggleSeguir(tienda: MostrarTiendaPublicaDTO): void {
    const url = tienda.estaSiguiendo
      ? `http://localhost:5206/api/Seguidor/quitar/${tienda.id}`
      : `http://localhost:5206/api/Seguidor/seguir/${tienda.id}`;

    this.http.post(url, {}).subscribe({
      next: () => {
        tienda.estaSiguiendo = !tienda.estaSiguiendo;
      },
      error: (err) => {
        console.error('Error al seguir/dejar de seguir', err);
      }
    });
  }
}
