import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/local";
import { Observable, tap } from "rxjs";
import moment from "moment";

@Injectable({
    providedIn: 'root'
})
export class AuthService 
{
    baseUrl = `${environment.baseUrl}/auth-service`;

    constructor(private httpClient: HttpClient) { }

    login(email: string, password: string): Observable<any> {
        return this.httpClient
            .post<TokenResponse>(`${this.baseUrl}/login`, { email, password })
            .pipe(tap(this.setSession));
    }

    private setSession(loginResponse: TokenResponse): void {
        var expiresAt = moment().seconds(loginResponse.expiresIn);
        localStorage.setItem('expiresAt', expiresAt.toString());
        localStorage.setItem('expiresIn', loginResponse.expiresIn.toString());
        localStorage.setItem('accessToken', loginResponse.accessToken);
        localStorage.setItem('refreshToken', loginResponse.refreshToken);
    }

    isAuthenticated(): boolean {
        //FIXME: remove expiresIn from local storage is not used       
        const token = localStorage.getItem('accessToken') ?? ``;
        const expiresAt = moment(localStorage.getItem('expiresAt'), `LLLL`);
        return token != `` && moment().isBefore(expiresAt);
    }
}

export interface TokenResponse {
    tokenType: string;
    accessToken: string;
    expiresIn: number;
    refreshToken: string;
}