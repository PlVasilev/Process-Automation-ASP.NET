import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class ErrorInterceptorService implements HttpInterceptor {

  constructor(private toastrService: ToastrService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      retry(1),
      catchError((err) => {
        let message = "";
        if (err.status === 401) {
          message = "Invalid Credentials (Token, Username or Password)",
          console.log(message);
        }
        else if (err.status === 404) {
          message = "Item Not Found",
          console.log(message);
        } else if (err.status === 400) {
          message = "Bad Request",
          console.log(message);
        } else {
          message = "Unexpected ERROR"
        }
        this.toastrService.error(message)
        return throwError(err)
      })
    )
  }
}
