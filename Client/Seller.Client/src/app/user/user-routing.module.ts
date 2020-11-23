import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AuthGuard } from '../auth.guard';



const routes: Routes = [
    {
        path: 'user',
        children: [
            {
                path: '',
                pathMatch: 'full',
                redirectTo: '/user/login'
            },
            {
                path: 'login',
                component: LoginComponent,
                canActivate: [AuthGuard],
                data: {
                    isLogged: false
                }
            },
            {
                path: 'register',
                component: RegisterComponent,
                canActivate: [AuthGuard],
                data: {
                    isLogged: false
                }
            }
        ]
    }
];

export const UserRoutingModule = RouterModule.forChild(routes);