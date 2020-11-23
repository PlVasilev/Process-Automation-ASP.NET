import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ListingService } from '../listing.service'
import { ActivatedRoute, Router } from '@angular/router';
import { IListing } from 'src/app/shared/Interfaces/IListing';
import { UserService } from 'src/app/user/user.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {

  selectedListing: IListing;
  token: string;
  userId: string

  constructor(
    private listingService: ListingService,
    private activatedRoute: ActivatedRoute,
    private userService: UserService,
    private router: Router,
    private toastrService: ToastrService,
  ) { }

  @ViewChild('editListingForm', { static: true }) from: NgForm

  ngOnInit() {
    this.listingService.details(this.activatedRoute.snapshot.params.id)
      .subscribe(listing => {
        this.selectedListing = listing
        this.userId = this.userService.getUserId()
        this.token = this.userService.getToken()
      });
  }

  updateListingHandler(data: IListing) {
    data.id = this.activatedRoute.snapshot.params.id;
    this.listingService.edit(data).subscribe(res => {
      this.from.reset();
      this.router.navigate([`/listing/details/${this.selectedListing.id}`])
      this.toastrService.success("Listing Edited")
    });
  }
}
