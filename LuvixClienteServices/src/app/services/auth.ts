// src/app/services/auth.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';


// Interfaz para registro
export interface RegistroRequest {
  nombre: string;
  apellido: string;
  genero: string;
  email: string;
  password: string;
}

// Interfaz para la respuesta del login
export interface LoginResponse {
  mensaje: string;
  token: string;
}

@Injectable({
  providedIn: 'root'
})

export class AuthService {


  // URL de tu API .NET Core
  private apiUrl = 'http://localhost:5206/api/Acceso';

  // Clave bajo la cual se guarda el token en localStorage
  private tokenKey = 'jwt_token';

  constructor(private http: HttpClient) { }

  /**
   * Registra un nuevo usuario
   */
  registrar(data: RegistroRequest): Observable<any> {
    return this.http.post(`${this.apiUrl}/registrarse`, data).pipe(
      catchError(this.handleError)
    );
  }

  /**
   * Inicia sesión
   */
  login(email: string, password: string): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${this.apiUrl}/login`, { email, password }).pipe(
      catchError(this.handleError)
    );
  }

  /**
   * Guarda el token JWT en localStorage
   */
  setToken(token: string): void {
    localStorage.setItem(this.tokenKey, token);
  }

  /**
   * Obtiene el token almacenado
   */
  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  /**
   * Verifica si el token ha expirado
   */
  isTokenExpired(token: string): boolean {
    try {
      const payload = JSON.parse(atob(token.split('.')[1])); // Decodifica el payload
      const exp = payload.exp * 1000; // La fecha de expiración está en segundos → convierte a milisegundos
      return Date.now() > exp; // Compara con la fecha actual
    } catch {
      return true; // Si no se puede decodificar, asume que está expirado
    }
  }

  // isLoggedIn(): boolean {
  //   const token = this.getToken();
  //   return !!token && !this.isTokenExpired(token);
  // }

  private loggedIn = new BehaviorSubject<boolean>(false);

  get isLoggedIn(): Observable<boolean> {
    return this.loggedIn.asObservable();
  }


// Obtener nombre del usuario desde el Tokken
getNombre(): string | null {
  const token = this.getToken();
  if (!token) return null;

  try {
    const payload = JSON.parse(atob(token.split('.')[1]));

    // Si usaste "nombre" como claim personalizado
    if (payload['nombre']) {
      return payload['nombre'];
    }

    // Si solo tienes el nombre completo en 'name'
    const nombreCompleto = payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'];
    if (nombreCompleto) {
      return nombreCompleto.split(' ')[0]; // Toma solo la primera palabra
    }

    return null;
  } catch {
    return null;
  }
}

  /**
   * Cierra sesión (elimina el token)
   */
  logout(): void {
    localStorage.removeItem(this.tokenKey);
  }

  /**
   * Manejo de errores HTTP
   */
  private handleError(error: any) {
    const mensaje = error.error?.mensaje || 'Error en la autenticación';
    console.error('Auth Error:', mensaje);
    return throwError(() => new Error(mensaje));
  }
}
