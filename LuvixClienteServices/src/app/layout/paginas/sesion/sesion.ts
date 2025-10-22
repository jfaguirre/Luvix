import { Component } from '@angular/core';
import { FormSesion } from '../../componentes/form-sesion/form-sesion';

@Component({
  selector: 'app-sesion',
  imports: [FormSesion],
  templateUrl: './sesion.html',
  styleUrl: './sesion.css',
  standalone: true
})
export class Sesion {
}
