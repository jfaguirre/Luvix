import { Component, inject } from '@angular/core';
import { AuthService } from '../../../services/auth';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  selector: 'app-foto-perfil',
  imports: [CommonModule],
  templateUrl: './foto-perfil.html',
  styleUrl: './foto-perfil.css'
})
export class FotoPerfil {

  private http = inject(HttpClient);
  authService = inject(AuthService);

  fotoUrl = this.getFotoUrl();

  onFileSelected(event: any): void {
  const file: File = event.target.files[0];
  if (file) {
    const formData = new FormData();
    formData.append('file', file);

    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.authService.getToken()}`
    });

    this.http.put('http://localhost:5206/api/Usuario/subir-foto', formData, { headers }).subscribe({
      next: (response: any) => {
        this.fotoUrl = response.fotoUrl;
        console.log('Foto subida:', response.fotoUrl);
      },
      error: (err) => {
        console.error('Error al subir foto', err);
      }
    });
  }
}

  getFotoUrl() {
    const token = this.authService.getToken();
    if (token) {
      const payload = JSON.parse(atob(token.split('.')[1]));
      return payload['FotoPerfil'] || '/assets/fotos-perfil/default-perfil.jpg'; // imagen por defecto
    }
    return '/assets/fotos-perfil/default-perfil.jpg';
  }
}

