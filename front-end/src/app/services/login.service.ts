import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoginResponse } from '../types/login-response.types';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  private apiUrl = `${environment.apiUrl}/user/login`; // URL da API de login

  constructor(private httpClient: HttpClient) { }

  // Método de login
  login(email: string, password: string): Observable<LoginResponse> {
    return this.httpClient.post<LoginResponse>(this.apiUrl, { email, password });
  }

  // Verifica se o usuário está autenticado
  isAuthenticated(): boolean {
    return !!localStorage.getItem('auth-token'); // Ou sessionStorage.getItem('auth-token');
  }

  // Método para obter o token armazenado
  getToken(): string | null {
    if (typeof window === 'undefined') return null;
    return localStorage.getItem('auth-token');
  }
  

  // Método para armazenar o token
  storeToken(token: string): void {
    localStorage.setItem('auth-token', token); // Ou sessionStorage.setItem('auth-token', token);
  }

  // Método para remover o token (logout)
  logout(): void {
    localStorage.removeItem('auth-token'); // Ou sessionStorage.removeItem('auth-token');
  }
}
