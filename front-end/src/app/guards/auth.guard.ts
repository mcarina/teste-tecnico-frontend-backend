import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { LoginService } from '../services/login.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private loginService: LoginService, private router: Router) {}

  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    // Verifica se o usuário está autenticado
    if (this.loginService.isAuthenticated()) {
      return true;
    } else {
      // Redireciona para a página de login se não estiver autenticado
      this.router.navigate(['/']);
      return false;
    }
  }
}
