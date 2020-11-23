import { Injectable, Input } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { UserService } from './user/user.service';
import { ListingService } from './listing/listing.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  isLogged = false;
  isAdmin = false;
  isYours = false;

  constructor(
      private userService: UserService,
      private lisgingService: ListingService,
      private router: Router
      ) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    if(this.userService.getToken()){
        this.isLogged = true;
        if(this.userService.getUsername() === "admin") {
          this.isAdmin = true;
        }
        if(this.lisgingService.getCurrentListingSeller() === this.userService.getUserId()){
          this.isYours = true;
        }
    }
    if (this.isLogged === route.data.isLogged){   
        return true;
    }
    else if(this.isAdmin === route.data.isAdmin){
      return true;
    }
    else if(this.isYours === route.data.isYours){
      return true;
    }
    else{
        this.router.navigateByUrl('/notauthorized');
    }
  }
}