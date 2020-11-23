import { Component, OnInit, ViewChild } from '@angular/core';
import { ListingService } from '../../listing/listing.service';
import { IListing } from '../../shared/Interfaces/IListing';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from 'src/app/user/user.service';


@Component({
  selector: 'app-landing',
  templateUrl: './landing.component.html',
  styleUrls: ['./landing.component.css']
})
export class LandingComponent implements OnInit {

  listingId = null;

  constructor(private listingService: ListingService, private router: Router, private userService: UserService) { }

  get username(){return this.userService.getUsername()}
  get token(){return this.userService.getToken()}

  async ngOnInit() {
      // await this.listingService.getAllListings();
  }

  detailsIdHandler(listingId: string){
    this.router.navigate([`/listing/details/${listingId}`])
  }
  
  @ViewChild('searchInput', {static: true}) from: NgForm
  // @ViewChild('searchFrom', { static: true }) from: NgForm
  
   searchFormHandler(value){  
     // this.listingService.searchListings(value);
   }

  // detailsHandler(listing: IListing){
  //   this.listingService.selectedListing = listing;
  // }

  fromChild(event){
    this.listingId = event;
  }
}
