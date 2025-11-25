export interface CrearTiendaAPI {
  idCategoria: number,
  nombre: string,
  descripcion: string
}

export interface MostrarTiendaPublicaDTO {
  id: number;
  nombre: string;
  descripcion: string | null;
  logo: string | null;
  estaSiguiendo: boolean;
}

export interface TiendaSeguidaDTO {
  id: number;
  nombre: string;
  descripcion: string | null;
  logo: string | null;
}
