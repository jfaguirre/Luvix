import { Component } from '@angular/core';
import { Menu } from '../layout/componentes/menu/menu';
import { Footer } from '../layout/componentes/footer/footer';
import { RouterOutlet } from '@angular/router';

import { AuthService } from '../services/auth';
import { Observable } from 'rxjs';


@Component({
  standalone: true,
  selector: 'app-layout',
  imports: [RouterOutlet, Menu, Footer],
  templateUrl: './layout.html',
  styleUrl: './layout.css'
})
export class Layout {

}
