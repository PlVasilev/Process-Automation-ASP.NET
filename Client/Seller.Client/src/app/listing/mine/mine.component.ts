import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { ListingService } from '../listing.service';
import { IListing } from 'src/app/shared/Interfaces/IListing';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-mine',
  templateUrl: './mine.component.html',
  styleUrls: ['./mine.component.css']
})
export class MineComponent implements OnInit {

  listingId = null;
  allListings: Array<IListing>

  constructor(private listingService: ListingService, private router: Router) { }

  ngOnInit() {
        this.allListings = new Array();
       this.listingService.getMineListings().subscribe(listings => {
        this.allListings = listings
       });
  }

  detailsIdHandler(listingId: string){
    this.router.navigate([`/listing/details/${listingId}`])
  }
  
  @ViewChild('searchFrom', {static: true}) from: NgForm
  
   searchFormHandler(value){  
    this.listingService.getMineListings().subscribe(listings => {
      this.allListings = listings
      this.allListings = this.allListings.filter(x => x.title.includes(value.search))
     });
   
   }
  fromChild(event){
    this.listingId = event;
  }
}
