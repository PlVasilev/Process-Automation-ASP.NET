import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { UserModule } from './user/user.module';
import { CoreModule } from './core/core.module';
import { UserService } from './user/user.service';
import { ListingModule } from './listing/listing.module';
import { ListingService } from './listing/listing.service';
import { OfferModule } from './offer/offer.module';
import { OfferService } from './offer/offer.service';
import { TokenInterceptorService } from './shared/services/token-interceptor.service';
import { ErrorInterceptorService } from './shared/services/error-interceptor.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { MessageModule } from './message/message.module';
import { DealModule } from './deal/deal.module';
import { DealService } from './deal/deal.service';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    CoreModule,
    UserModule,
    ListingModule,
    OfferModule,
    MessageModule,
    DealModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot()
  ],
  providers: [
    UserService,
    ListingService,
    OfferService,
    DealService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptorService,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorInterceptorService,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
