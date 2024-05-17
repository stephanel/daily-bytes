import { Injectable } from "@angular/core";
import { Observable, of } from "rxjs";
import { User } from "src/app/users/user.model";

@Injectable({
    providedIn: 'root'
})
export class UserService {

    get(id: number): Observable<User> {
        return of<User>(
            {
                id: 74,
                firstName: 'John',
                lastName: 'Doe',
                email: 'john.doe@hype.com',
                phone: '+45 12 34 56 78',
                subscription: 'Premium',
                subscriptionExpiration: new Date('2024-09-15')
            }
        );
    }
}