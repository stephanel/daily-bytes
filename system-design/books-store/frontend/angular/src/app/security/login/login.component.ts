import { Component } from "@angular/core";
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from "@angular/forms";
import { AuthService } from "../auth.service";
import { ActivatedRoute, Router } from "@angular/router";

@Component({
    standalone: true,
    selector: 'login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css'],
    imports: [ReactiveFormsModule]
})
export class LoginComponent {

    loginForm: FormGroup;

    constructor(private formBuilder: FormBuilder,
        private authService: AuthService,
        private route: ActivatedRoute,
        private router: Router) {
        this.loginForm = this.formBuilder.group({
            email: ['', Validators.required],
            password: ['', Validators.required]
        });
    }

    formIsNotValid(): boolean {
        return this.loginForm.invalid && this.loginForm.touched;
    }

    onSubmit(): boolean {
        const email = this.loginForm.get('email')?.value;
        const password = this.loginForm.get('password')?.value;

        if(email == `` || password == ``)
        {
            return false;
        }

        this.authService.login(email, password)
            .subscribe(
                () => {
                    // FIXME: redirect to caller page
                    console.log('User is authenticated');
                    const returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
                    this.router.navigate(returnUrl);
                }, 
                (error) => {
                    throw error;
                });

        // if(this.authService.isAuthenticated())
        // {
        //     console.log('User is authenticated');            
        //     return true;
        // }
        
        return false;
    }
}