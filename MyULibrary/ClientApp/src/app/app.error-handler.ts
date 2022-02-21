import { ErrorHandler, Injectable, NgZone } from "@angular/core";
import { NotificationService } from "./services/notification.service";

@Injectable()
export class AppErrorHandler implements ErrorHandler {
  constructor(
    private ngZone: NgZone, 
    private notificationService: NotificationService) { }

  handleError(error: any): void {
    this.ngZone.run(() => {
      this.notificationService.showError(error.message, "Error");
    });
  }
}