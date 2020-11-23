import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/user/user.service';
import { AuthGuard } from 'src/app/auth.guard';
import { NotificationsService } from 'src/app/shared/services/notifications.service';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css']
})
export class FooterComponent implements OnInit {

  constructor( private userService: UserService,
    private authGuard: AuthGuard,  
    private notificationsService: NotificationsService) { }

  ngOnInit(): void {
    this.token;
    this.notificationsService.subscribe();
  }
  get username(){return this.userService.getUsername()}
  get token(){return this.userService.getToken()}

}
