import React, { useState, useEffect } from 'react';
import axios from 'axios';
import {MarkedProvider} from './MarkedContext';
import logo from './Images/logo.png';
import githubLogo from './Images/github-logo.png';
import './Stylesheets/App.css';
import Home from './components/Home';
import Marked from './components/JobComponents/Marked';
import Statistics from './components/JobComponents/JobStatistics';
import SearchResults from './components/JobComponents/SearchResults'
import Detail from './components/JobComponents/JobDetail'
import Register from './components/AuthComponents/Register'
import Login from './components/AuthComponents/Login'

import {
  BrowserRouter as Router,
  Switch,
  Route,
  Link
} from "react-router-dom";

function App() {

    const [jobs, setJobs] = useState([]);
    const [username, setUsername] = useState([]);
    const [isLoggedIn, setIsLoggedIn] = useState([false]);

  let markedJobs = [];

  function handleChange(markedJob) {
    markedJobs.push(markedJob)
    console.log(markedJobs)
  }

  const NavElementStyle = {
    fontSize: 20,
    display: "inline-block",
    margin: "5px",
    padding: "3px",
    borderRadius: "15px",
    textDecoration: 'none',
    backgroundColor: "#78c3ff",
    width: "180px",
    hight: "40px"
    }

    useEffect(() => {
        getJobs();
        getUser();
    }, []);


    const getJobs = () => {
        axios.get('/api').then(data => setJobs(data.data))
    }

    const getUser = () => {
        axios.get('/getCookieData').then(data => {
             if ((data.data).length != 0) {
                 setIsLoggedIn(true)
                 setUsername(data.data)
                }
            });
    }
    return (
    <MarkedProvider>
    <div className="App">
    <header className="App-header">
                  <img src={logo} className="App-logo" alt="logo" /> 
    </header>
      
      <Router>
      <div className="cardsDiv">
            <nav>
            <ul>
            <li  style={NavElementStyle}>
              <img src={githubLogo} className="Git-logo" alt="logo" />
              <Link to="/" style={{color:'black'}}>Home</Link>
              <img src={githubLogo} className="Git-logo" alt="logo" />
                </li>
             <li style={NavElementStyle}>
              <img src={githubLogo} className="Git-logo" alt="logo" />
              <Link to="/marked" style={{color:'black'}}>Marked jobs</Link>
              <img src={githubLogo} className="Git-logo" alt="logo" />
            </li>
            <li style={NavElementStyle}>
              <img src={githubLogo} className="Git-logo" alt="logo" />
              <Link to="/search" style={{color:'black'}}>Search</Link>
              <img src={githubLogo} className="Git-logo" alt="logo" />
            </li>
            <li style={NavElementStyle}>
              <img src={githubLogo} className="Git-logo" alt="logo" />
              <Link to="/registration" style={{color:'black'}}>Register</Link>
              <img src={githubLogo} className="Git-logo" alt="logo" />
            </li>
            <li style={NavElementStyle}>
              <img src={githubLogo} className="Git-logo" alt="logo" />
              <Link to="/login" style={{color:'black'}}>Login</Link>
              <img src={githubLogo} className="Git-logo" alt="logo" />
            </li>
            <li style={NavElementStyle}>
              <img src={githubLogo} className="Git-logo" alt="logo" />
              <Link to="/statistics" style={{color:'black'}}>Statistics</Link>
              <img src={githubLogo} className="Git-logo" alt="logo" />
            </li>
                              {isLoggedIn === true &&
                                    <li style={NavElementStyle}>
                                    <Link to="/user" style={{ color: 'black' }}>User:{username}</Link>
                                  </li>
                              }
                              {isLoggedIn === true &&
                                    <li style={NavElementStyle}>
                                  <form action="/logout">
                                      <button type="submit" className="buttonLogout">Logout</button>
                                  </form>
                                  </li>
                              }
                                  
          </ul>
        </nav>

        {/* A <Switch> looks through its children <Route>s and
            renders the first one that matches the current URL. */}
        <Switch>
          <Route path="/search">
            <SearchResults />
          </Route>
          <Route path="/login">
            <Login />
          </Route>
          <Route path="/marked">
            <Marked markedJobs={markedJobs}/>
          </Route>
          <Route path="/detail">
            <Detail/>
          </Route>
          <Route path='/registration'>
            <Register/>
          </Route>
          <Route path="/statistics">
            <Statistics />
          </Route>
            <Route path="/">
            <Home jobs={jobs} onChange={handleChange}/> 
          </Route>
        </Switch>
      </div>
    </Router>
    <br></br>
    </div>
    </MarkedProvider>
  );
}

export default App;
