import React, { useState,useEffect } from 'react';
import axios from 'axios';
import DetailDesc from './DetailsDescriptionConverter'

export default function Detail() {
    const url = new URL(window.location.href);
    const jobId = url.searchParams.get('id');

    const [job, setJob] = useState([])

    useEffect(() => {
        getJobById();
    }, [])

    const getJobById = () => {
        axios.get('/api/' + jobId).then(job => {
            setJob(job.data)
        })
    }

    return (
        <ul className="list-group" style={{ position: 'absolute', textAlign: 'left' }}>
            <li className="list-group-item" style={{fontSize: '25px'}}><strong><img style={{width:'200px', height:'auto'}} src={job.companyLogo} alt={job.company}></img></strong></li>
            <li className="list-group-item" style={{ fontSize: '20px' }}><strong>Title: </strong> {job.title}</li>
            <li className="list-group-item" style={{fontSize: '20px'}}><strong>Location: </strong> : {job.location}</li>
            <li className="list-group-item" style={{wordSpacing: '5px'}}><strong>Description: </strong>  <DetailDesc DetailDesc={job.description} /></li>
        </ul>
    )
}


