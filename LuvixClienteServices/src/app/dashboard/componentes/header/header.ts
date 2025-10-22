import { Component } from '@angular/core';
import { FotoPerfil } from '../foto-perfil/foto-perfil';
import { AuthService } from '../../../services/auth';

@Component({
  standalone:true,
  selector: 'app-header',
  imports: [FotoPerfil],
  templateUrl: './header.html',
  styleUrl: './header.css'
})
export class Header {

  constructor(public authService: AuthService) {}
}
