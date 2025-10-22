import { Component } from '@angular/core';
import { Header } from '../../componentes/header/header';


@Component({
  selector: 'app-inicio',
  standalone: true,
  imports: [Header],
  templateUrl: './inicio.html',
  styleUrl: './inicio.css'
})
export class Inicio {

}
