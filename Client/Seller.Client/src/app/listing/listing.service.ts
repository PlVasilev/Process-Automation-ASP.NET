import { Injectable, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http'
import { Observable } from 'rxjs';
import { environment } from "../../environments/environment"
import { IListing } from '../shared/Interfaces/IListing';

@Injectable({
  providedIn: 'root'
})
export class ListingService {

  constructor(private http:HttpClient) { }
 

  private createPath = environment.listingsApiUrl + 'listing/create'
  private getAllPath = environment.listingsApiUrl + 'listing/all'
  private getMinePath = environment.listingsApiUrl + 'listing/mine'
  private detailsPath = environment.listingsApiUrl + 'listing/'
  private updatePath = environment.listingsApiUrl + 'listing/update'

  setCurrentListingSeller(sellerId){
    localStorage.setItem('currentListingSeller', sellerId)
  }

  getCurrentListingSeller(){
    return localStorage.getItem('currentListingSeller');
  }
  
  create(data): Observable<IListing>{
    return this.http.post<IListing>(this.createPath, data)
  }

  getListings(): Observable<Array<IListing>>{
    return this.http.get<Array<IListing>>(this.getAllPath)
  }

  getMineListings(): Observable<Array<IListing>>{
    return this.http.get<Array<IListing>>(this.getMinePath)
  }

  details(id): Observable<IListing>{
    return this.http.get<IListing>(this.detailsPath + id);
  }

  delete(id){
    return this.http.delete(this.detailsPath + id)
  }

  edit(data): Observable<IListing>{
    return this.http.put<IListing>(this.updatePath, data)
  }
}
