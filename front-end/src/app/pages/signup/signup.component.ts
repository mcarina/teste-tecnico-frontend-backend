import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ReactiveFormsModule, FormGroup, FormControl, Validators } from '@angular/forms';
import { LoginLayoutComponent } from '../../components/login-layout/login-layout.component';
import { PrimaryInputComponent } from '../../components/primary-input/primary-input.component';
import { UserService } from '../../services/user.service'; // Alterado para userService com "u" minúsculo

@Component({
  selector: 'app-signup',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    LoginLayoutComponent,
    PrimaryInputComponent
  ],
  templateUrl: './signup.component.html',
  styleUrl: './signup.component.css'
})
export class SignupComponent {
  signupForm: FormGroup;

  constructor(
    private router: Router,
    private userService: UserService, // Alterado para userService com "u" minúsculo
    private toastService: ToastrService
  ) {
    this.signupForm = new FormGroup({
      name: new FormControl('', [Validators.required]),
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required, Validators.minLength(4)]),
      passwordConfirm: new FormControl('', [Validators.required, Validators.minLength(4)])
    });
  }

  submit() {
    if (this.signupForm.value.password !== this.signupForm.value.passwordConfirm) {
      this.toastService.error("Passwords do not match");
      return;
    }

    const { name, email, password } = this.signupForm.value;

    this.userService.postUsers(name, email, password).subscribe({
      next: () => {
        this.toastService.success("User registered successfully!");
        this.router.navigate([""]); // Redireciona para o login
      },
      error: (err: any) => {
        console.error(err);
        // Aqui, melhor exibir a mensagem de erro do backend, se disponível
        this.toastService.error(err.error?.message || "Error registering user");
      }
    });
  }

  navigate() {
    this.router.navigate([""]);
  }
}
