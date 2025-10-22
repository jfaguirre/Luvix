import { Component } from '@angular/core';
import { Menu } from '../dashboard/componentes/menu/menu';
import { RouterOutlet } from '@angular/router';
import { Header } from '../dashboard/componentes/header/header';

@Component({
  standalone: true,
  selector: 'app-dashboard',
  imports: [RouterOutlet, Header, Menu],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css'
})
export class Dashboard {

}
