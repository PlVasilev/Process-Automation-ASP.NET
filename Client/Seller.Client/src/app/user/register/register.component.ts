import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { UserService } from 'src/app/user/user.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {

  constructor(
    private userService: UserService,
    private toastrService: ToastrService,
    private router: Router) { }

  ngOnInit() {
  }
  registerHandler(formValue) {
    const { username, password } = formValue;
    const userData = { username, password };
    console.log(formValue.username)
    this.userService.register(userData).subscribe(res => {
      this.userService.saveToken(res['token'], res['username'])
      const { username, firstName, lastname, email, phoneNumber } = formValue;
      const sellerData = { username, firstName, lastname, email, phoneNumber };
      this.userService.createSeller(sellerData).subscribe(res => {
        this.userService.getSellerId().subscribe(res => {
          this.userService.saveSellerId(res['id'])
        })
        this.toastrService.success("Registered")
        this.router.navigate([`/listing/all`])
      })
    })
  }
}
