import { CrearUsuarioAPI } from './../../interface/usuarios.models';
import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})

export class UsuarioService {

  constructor() {}

  private http = inject(HttpClient);
  private URLbase = environment.apiURL + '/api/Acceso/registrarse';

  public crear(usuario: CrearUsuarioAPI)
  {
    return this.http.post(this.URLbase, usuario)
  }
}
