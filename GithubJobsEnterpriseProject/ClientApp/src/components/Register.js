import React, { useState, useEffect } from 'react'
import axios from 'axios';
import Login from './Login';

export default function Register() {

    const [username, setUsername] = useState([]);
    const [password, setPassword] = useState([]);
    const [passwordRe, setPasswordRe] = useState([]);
    const [email, setEmail] = useState([]);
    const [isValidUsername, setIsValidUsername] = useState(false);
    const [isValidEmail, setIsValidEmail] = useState(false);
    const [isCredentailsValid, setIsCredentailsValid] = useState(null);

    const handleChange = (e) => {
        if (e.target.name == "Username") {
            if (e.target.value !== null && e.target.value.length < 6) {
                setUsername(e.target.value);
                setIsValidUsername(false);
            } else {
                setUsername(e.target.value);
                setIsValidUsername(true);
            }
        }
        else if (e.target.name == "Email") {
            if (e.target.value !== null && e.target.value.length < 12) {
                setUsername(e.target.value);
                setIsValidEmail(false);
            } else {
                setEmail(e.target.value);
                setIsValidEmail(true);
            }
        }
        else if (e.target.name == "Password") {
            setPassword(e.target.value);
        } else {
            setPasswordRe(e.target.value);
        }

    };

    const handleSubmit = (event) => {
        event.preventDefault();
        axios.get("/username=" + username + "&email=" + email + "&password=" + password).then(data => { setIsCredentailsValid(data.data) });
    }

    return (
        <form onSubmit={handleSubmit} style={{ padding: '5%', border: '1px solid #ced4da', marginBottom: '2%'}}>
                <div class="container register-form">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <input type="text" class="form-control" onChange={handleChange} name='Username' placeholder="Your Username *" />
                                {isValidUsername === false && username.length > 1 &&
                                    <p style={{color : "red"}}>Invalid Username!</p>
                                }
                            </div>
                            <div class="form-group">
                                <input type="text" class="form-control" onChange={handleChange} name='Email' placeholder="Your Email *" />
                                {isValidEmail === false && email.length > 1 &&
                                    <p style={{ color: "red" }}>Invalid Email!</p>
                                }
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <input type="password" class="form-control" onChange={handleChange} name='Password' placeholder="Your Password *"/>
                            </div>
                            <div class="form-group">
                                <input type="password" class="form-control" onChange={handleChange} name='PasswordRe' placeholder="Confirm Password *"/>
                            </div>
                            {password !== passwordRe && password.length > 1 && passwordRe.length > 1 &&
                                <p style={{ color: "red" }}>Passwords does not match!</p>
                            }
                        </div>
                    </div>
            </div>
            {isCredentailsValid === true &&
                <p style={{ color: "green" }}>Success!</p>
            }
            {isCredentailsValid === false &&
                <p style={{ color: "red" }}>Email or username is taken!</p>
            }
            {isValidUsername === true && isValidEmail === true && password === passwordRe &&
                <button type="submit" style={{
                    border: 'none', borderRadius: '1.5rem',
                    padding: '1%', width: '12%', cursor: 'pointer', background: '#0062cc', color: '#fff'
                }}>Register</button>
            }
            </form>
    )
}
