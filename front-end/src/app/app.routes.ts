import { Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { SignupComponent } from './pages/signup/signup.component';
import { HomeComponent } from './pages/home/home.component';
import { AuthGuard } from './guards/auth.guard';

export const routes: Routes = [
    {
        path: "",
        component: LoginComponent
    },
    {
        path:"sign-up",
        component: SignupComponent
    },
    {
        path:"home",
        component: HomeComponent,
        canActivate: [AuthGuard]
    }
];
