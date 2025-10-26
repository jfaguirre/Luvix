// src/app/layout/componentes/menu/menu.ts
import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AuthService } from '../../../services/auth';
import { Observable } from 'rxjs';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  selector: 'app-menu',
  imports: [RouterModule, CommonModule],
  templateUrl: './menu.html',
  styleUrl: './menu.css',
})
export class Menu {

  // constructor(public authService: AuthService) {}

}
