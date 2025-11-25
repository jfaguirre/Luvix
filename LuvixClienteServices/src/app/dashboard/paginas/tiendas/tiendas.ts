import { Component } from '@angular/core';
import { TiendasPublicas } from "../../componentes/tiendas-publicas/tiendas-publicas";

@Component({
  selector: 'app-tiendas',
  imports: [TiendasPublicas],
  templateUrl: './tiendas.html',
  styleUrl: './tiendas.css',
  standalone: true,
})
export class Tiendas {

}
