import React, { useState, useEffect } from 'react'
import axios from 'axios';
import { Pie } from "react-chartjs-2";
import { MDBContainer } from "mdbreact";
import '../../Stylesheets/Statistics.css';

export default function Statistics() {
    const [pythonJobs, setPythonJobs] = useState([]);
    const [javaJobs, setJavaJobs] = useState([]);
    const [devopsJobs, setDevopsJobs] = useState([]);
    const [csharpJobs, setCsharpJobs] = useState([]);
    const [javascriptJobs, setJavascriptJobs] = useState([]);

    useEffect(() => {
        getPythonJobs();
        getJavaJobs();
        getDevopsJobs();
        getCsharpJobs();
        getJavascriptJobs();
    }, []);
        

    const getPythonJobs = () => {
        axios.get('/statistics/keyword=python').then(data => setPythonJobs(data.data))
    }

    const getJavaJobs = () => {
        axios.get('/statistics/keyword=java').then(data => setJavaJobs(data.data))
    }

    const getDevopsJobs = () => {
        axios.get('/statistics/keyword=devops').then(data => setDevopsJobs(data.data))
    }

    const getCsharpJobs = () => {
        axios.get('/statistics/keyword=.net').then(data => setCsharpJobs(data.data))
    }

    const getJavascriptJobs = () => {
        axios.get('/statistics/keyword=javascript').then(data => setJavascriptJobs(data.data))
    }

    const diagram = {
        dataPie: {
          labels: ["Python", "JavaScript", "Java", "DevOps", ".Net"],
          datasets: [
            {
                  data: [pythonJobs, javaJobs, devopsJobs, csharpJobs, javascriptJobs],
              backgroundColor: [
                "#F7464A",
                "#46BFBD",
                "#FDB45C",
                "#949FB1",
                "#4D5360",
                "#AC64AD"
              ],
              hoverBackgroundColor: [
                "#FF5A5E",
                "#5AD3D1",
                "#FFC870",
                "#A8B3C5",
                "#616774",
                "#DA92DB"
              ]
            }
          ]
        }
      }

    return (
        <div className="Chart-Container">
            <MDBContainer style={{width:"1000px"}} >
        <h3 className="mt-5">Statistics about available jobs in the top technologies</h3>
        <Pie data={diagram.dataPie} options={{ responsive: true }} />
        </MDBContainer>
        </div>
    )
}
