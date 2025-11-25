import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import { CrearTiendaAPI } from '../../interface/tienda.models';

@Injectable({
  providedIn: 'root'
})

export class TiendaServices {

  constructor() {}

  private http = inject(HttpClient);
  private URLbase = environment.apiURL + '/api/Tienda/crear-tienda';

  public crear(tienda: CrearTiendaAPI)
  {
    return this.http.post(this.URLbase, tienda)
  }
}
