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

  fotoUrl: string = '/fotos-perfil/default-perfil.jpg';

  // fotoUrl = this.cargarFotoPerfil();
  ngOnInit() {
    this.cargarFotoPerfil();
  }

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

  // Método para cargar la foto de perfil desde el backend
    cargarFotoPerfil(): void {
      const token = this.authService.getToken();
      if (!token) return;

      try {
        const payload = JSON.parse(atob(token.split('.')[1]));
        const userId = payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'];

        // Llama al backend para obtener la foto del usuario
        this.http.get(`http://localhost:5206/api/Usuario/perfil/${userId}`).subscribe({
          next: (response: any) => {
            this.fotoUrl = `http://localhost:5206${response.fotoUrl}` || '/fotos-perfil/default-perfil.jpg';
          },
          error: (err) => {
            console.error('Error al cargar la foto de perfil', err);
            this.fotoUrl = '/fotos-perfil/default-perfil.jpg';
          }
        });
      } catch (error) {
        console.error('Error al decodificar el token', error);
      }
  }
}
