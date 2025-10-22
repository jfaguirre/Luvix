export interface SesionAPI {
  email: string,
  password: string
}

export interface LoginResponse {
  mensaje: string;
  token: string;
}
