import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { NotFoundComponent } from './core/not-found/not-found.component';
import { LandingComponent } from './core/landing/landing.component';
import { NotAuthorizedComponent } from './core/not-authorized/not-authorized.component';
import { EditComponent } from './listing/edit/edit.component';
import { AuthGuard } from './auth.guard';
import { AllComponent } from './offer/all/all.component';
import { AddComponent } from './message/add/add.component';
import { MineComponent } from './offer/mine/mine.component';



const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: LandingComponent
  },
  {
    path: 'offer/mine',
    component: MineComponent,
    canActivate: [AuthGuard],
    data: {
        isLogged: true
    },
  },
  {
    path: 'message/add',
    component: AddComponent,
    canActivate: [AuthGuard],
    data: {
        isLogged: true
    }
},
  {
    path: 'listing/details/:id/offer/all',
    component: AllComponent,
    canActivate: [AuthGuard],
    data: {
        isYours: true  
    }
},
  {
    path: 'listing/edit/:id',
    component: EditComponent,
    canActivate: [AuthGuard],
    data: {
      isYours: true
    }
  },
  {
    path: 'notauthorized',
    component: NotAuthorizedComponent
  },
  {
    path: '**',
    component: NotFoundComponent
  }
];

export const AppRoutingModule = RouterModule.forRoot(routes);
