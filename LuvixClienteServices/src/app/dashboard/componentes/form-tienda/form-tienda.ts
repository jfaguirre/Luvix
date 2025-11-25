import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { nombreValidator, maxLengthCustom, minLengthCustom } from '../../../validaciones/validaciones';
import { CrearTiendaAPI } from '../../../interface/tienda.models';
import { TiendaServices } from '../../../services/tienda-services/tienda-services';
import { HttpClient } from '@angular/common/http';


@Component({
  selector: 'app-form-tienda',
  imports: [ReactiveFormsModule],
  templateUrl: './form-tienda.html',
  styleUrl: './form-tienda.css',
  standalone: true
})

export class FormTienda {

  categorias: any[] = [];
  control: any;

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.http.get<any>('http://localhost:5206/api/Tienda/lista-categorias').subscribe({
      next: (response) => {
        this.categorias = response.value;
      },
      error: (err) => {
        console.error('Error al cargar categorías', err);
      }
    });
  }

  private readonly formBuilder = inject(FormBuilder);
  tiendaService = inject(TiendaServices);
  router = inject(Router);

  tiendaForm: FormGroup = new FormGroup({

          idCategoria: new FormControl("", [
          Validators.required,
          ]),

          nombre: new FormControl("", [
          Validators.required,
          minLengthCustom(3),
          maxLengthCustom(50),
          nombreValidator()]),

          descripcion: new FormControl ("", [
          Validators.required,
          minLengthCustom(20),
          maxLengthCustom(150)

          ])
  });

  CrearTienda()
  {
    const formValue = this.tiendaForm.value;

    const tienda: CrearTiendaAPI = {
      idCategoria: formValue.idCategoria,
      nombre: formValue.nombre,
      descripcion: formValue.descripcion
    };

    this.tiendaService.crear(tienda).subscribe({
      next: () => {
        this.router.navigate(['/dashboard/perfil']);
      },
      error: (err) => {
        console.error('Error al crear la tienda:', err);
      }
    });
  }


}
