import { Component } from '@angular/core';
import { FormRegistro } from '../../componentes/form-registro/form-registro';

@Component({
  selector: 'app-registro',
  imports: [FormRegistro],
  templateUrl: './registro.html',
  styleUrl: './registro.css',
  standalone: true,
})
export class Registro {

}
