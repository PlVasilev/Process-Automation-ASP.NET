import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/user/user.service';
import { Router } from '@angular/router';
import { ToastrService } from '../../../../node_modules/ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'] 
})
export class LoginComponent {

  constructor(
    private userService: UserService,
    private router: Router, 
    private toastrService: ToastrService) { }

  loginHandler(formValue) {
    this.userService.login(formValue).subscribe(data => {
      this.userService.saveToken(data['token'],data['username'])
      this.userService.getSellerId().subscribe(res => {   
        this.userService.saveSellerId(res['id'])
        this.toastrService.success("Loging In")
        this.router.navigate([`/listing/all`])
      })
    })
  }

}
