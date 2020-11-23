import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from 'src/app/user/user.service';
import { MessageService } from '../message.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.css']
})
export class AddComponent implements OnInit {


  constructor(private router: Router,private toastrService: ToastrService, private userSerive: UserService, private messageService: MessageService) { }

  @ViewChild('addMessageForm', { static: true }) from: NgForm;

  ngOnInit() {
  }

  addMessageHandler(data) {
    const messageData = {
      senderId: this.userSerive.getUserId(),
      senderUsername: this.userSerive.getUsername(),
      title: data['title'],
      content: data['content'],
    }
    this.messageService.addMessage(messageData).subscribe(res => {
    })
    this.from.reset();
    this.router.navigate(['listing/all']);
    this.toastrService.success("Message send")
  }
}
