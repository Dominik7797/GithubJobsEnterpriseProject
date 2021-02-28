import React, { useState } from 'react'
import axios from 'axios'

export default function Login() {
    const [username, setUsername] = useState([]);
    const [password, setPassword] = useState([]);
    const [isCredentailsValid, setIsCredentailsValid] = useState(null);

    const handleChange = (e) => {
        if (e.target.name === "Username") {
            setUsername(e.target.value);
        }
        
        else if (e.target.name === "Password") {
            setPassword(e.target.value);
        }

    };

    const handleSubmit = (event) => {
        event.preventDefault();
        axios.get("/login/username=" + username + "&password=" + password).then(data => setIsCredentailsValid(data.data))
    }

    return (
            <form method="POST" action="/login" onSubmit={handleSubmit} style={{ padding: '5%', border: '1px solid #ced4da', marginBottom: '2%'}}>
                <div className="container login-form">
                    <div className="row">
                        <div className="col-md-6">
                            <div className="form-group">
                                <input type="text" className="form-control" onChange={handleChange} name='Username' placeholder="Your Username *"/>
                            </div>
                        </div>
                        <div className="col-md-6">
                            <div className="form-group">
                                <input type="password" className="form-control" onChange={handleChange} name='Password' placeholder="Your Password *"/>
                            </div>
                        </div>
                    </div>
                </div>
                {isCredentailsValid === true &&
                    <p style={{ color: "green" }}>Success!</p>
                }
                {isCredentailsValid === false &&
                    <p style={{ color: "red" }}>Email or username is invalid!</p>
                }
                <button type="submit" style={{ border: 'none', borderRadius:'1.5rem',
                 padding: '1%',  width: '12%', cursor: 'pointer', background: '#0062cc', color: '#fff'}}>Login</button>
            </form>
    )
}
