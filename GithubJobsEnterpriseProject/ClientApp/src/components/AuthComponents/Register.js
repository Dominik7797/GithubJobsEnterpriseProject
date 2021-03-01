import React, { useState } from 'react'
import axios from 'axios';

export default function Register() {

    const [username, setUsername] = useState([]);
    const [password, setPassword] = useState([]);
    const [passwordRe, setPasswordRe] = useState([]);
    const [email, setEmail] = useState([]);
    const [isValidUsername, setIsValidUsername] = useState(false);
    const [isValidEmail, setIsValidEmail] = useState(false);
    const [isCredentailsValid, setIsCredentailsValid] = useState(null);

    const validateInputFiledLength = (value, minValue) => {
        if (value < minValue) {
            return false;
        }else{
            return true;
        }
    }

    const handleUserInputChange = (e) => {
        let currentField = e.target.name;
        let inputValue = e.target.value;

        let minimumUsernameLength = 6;
        let minimumEmailLength = 12;

        if (currentField == "Username") {
            if (!validateInputFiledLength(inputValue.length, minimumUsernameLength)) {
                setUsername(inputValue);
                setIsValidUsername(false);
            } else {
                setUsername(inputValue);
                setIsValidUsername(true);
            }
        }
        else if (currentField == "Email") {
            if (!validateInputFiledLength(inputValue.length, minimumEmailLength)) {
                setEmail(inputValue);
                setIsValidEmail(false);
            } else {
                setEmail(inputValue);
                setIsValidEmail(true);
            }
        }
        else if (currentField == "Password") {
            setPassword(inputValue);
        } else {
            setPasswordRe(inputValue);
        }

    };

    const formSubmit = (event) => {
        event.preventDefault();
        console.log(username);
        axios.get(`/verify/username=${username}&email=${email}&password=${password}`).then(data => { setIsCredentailsValid(data.data) });
    }

    return (
        <form onSubmit={formSubmit} style={{ padding: '5%', border: '1px solid #ced4da', marginBottom: '2%'}}>
                <div className="container register-form">
                    <div className="row">
                        <div className="col-md-6">
                            <div className="form-group">
                                <input type="text" className="form-control" onChange={handleUserInputChange} name='Username' placeholder="Your Username *" />
                                {isValidUsername === false && username.length > 1 &&
                                    <p style={{color : "red"}}>Invalid Username!</p>
                                }
                            </div>
                            <div className="form-group">
                                <input type="text" className="form-control" onChange={handleUserInputChange} name='Email' placeholder="Your Email *" />
                                {isValidEmail === false && email.length > 1 &&
                                    <p style={{ color: "red" }}>Invalid Email!</p>
                                }
                            </div>
                        </div>
                        <div className="col-md-6">
                            <div className="form-group">
                                <input type="password" className="form-control" onChange={handleUserInputChange} name='Password' placeholder="Your Password *"/>
                            </div>
                            <div className="form-group">
                                <input type="password" className="form-control" onChange={handleUserInputChange} name='PasswordRe' placeholder="Confirm Password *"/>
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
