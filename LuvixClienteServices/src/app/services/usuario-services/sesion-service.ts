import { SesionAPI, LoginResponse } from './../../interface/sesion.models';
import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.development';


@Injectable({
  providedIn: 'root'
})
export class SesionService {

  constructor() {}

  private http = inject(HttpClient);
  private URLbase = environment.apiURL + '/api/Acceso/login';

  public sesion(sesionUsuario: SesionAPI)
  {
    return this.http.post<LoginResponse>(this.URLbase, sesionUsuario)
  }

}
