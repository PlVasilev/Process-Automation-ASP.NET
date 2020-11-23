import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MineComponent } from './mine/mine.component';
import { AuthGuard } from '../auth.guard';



const routes: Routes = [
    {
        path: 'deal',
        children: [
            {
                path: '',
                pathMatch: 'full',
                redirectTo: '/deal/mine'
            },
            {
                path: 'mine',
                component: MineComponent,
                canActivate: [AuthGuard],
                data: {
                    isLogged: true
                }
            },
        ]
    }
];

export const DealRoutingModule = RouterModule.forChild(routes);