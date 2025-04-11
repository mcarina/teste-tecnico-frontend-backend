import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ReactiveFormsModule, FormGroup, FormControl, Validators } from '@angular/forms';
import { LoginLayoutComponent } from '../../components/login-layout/login-layout.component';
import { PrimaryInputComponent } from '../../components/primary-input/primary-input.component';
import { LoginService } from '../../services/login.service';

interface loginForm{
  email: FormControl,
  password: FormControl
}

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    LoginLayoutComponent,
    ReactiveFormsModule,
    PrimaryInputComponent
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})


export class LoginComponent {
  loginForm: FormGroup;

  constructor(
    private router: Router,
    private loginService: LoginService,
    private toastService: ToastrService
  ) {
    this.loginForm = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required, Validators.minLength(4)])
    });
  }


  
  submit() {
    if (this.loginForm.valid) {
      const { email, password } = this.loginForm.value;
  
      this.loginService.login(email, password).subscribe({
        next: (response: any) => {
          this.loginService.storeToken(response.token);
          this.toastService.success("Login successfully!");
          this.router.navigate(['/home']);
        },
        error: (error: any) => {
          this.toastService.error("Error when logging in");
          console.error("Error when logging in:", error);
        }
      });
    } else {
      this.toastService.warning("Fill in all fields correctly!");
    }
  }

  navigate(){
    this.router.navigate(["/sign-up"]);
  }

}
