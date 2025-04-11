import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = `${environment.apiUrl}/user`;

  constructor(private http: HttpClient) {}

  // Método para pegar todos os usuários
  getUsers(): Observable<any> {
    const token = localStorage.getItem('auth-token');
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });

    return this.http.get(this.apiUrl, { headers });
  }

  // Método para cadastrar um novo usuário
  postUsers(name: string, email: string, password: string): Observable<any> {
    return this.http.post(this.apiUrl, {
      name,
      email,
      password
    });
  }

  updateUser(id: string, data: { name?: string; email?: string; password?: string }): Observable<any> {
    const token = localStorage.getItem('auth-token');
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
  
    return this.http.put(`${this.apiUrl}/${id}`, data, { headers });
  }

  deleteUser(id: string): Observable<any> {
    const token = localStorage.getItem('auth-token');
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
  
    return this.http.delete(`${this.apiUrl}/${id}`, { headers });
  }
}
