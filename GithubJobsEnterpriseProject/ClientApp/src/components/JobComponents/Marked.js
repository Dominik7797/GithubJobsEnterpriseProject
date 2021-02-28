import React, { useState, useContext } from 'react';
import Job from './Job';
import {MarkedContext} from '../../MarkedContext';

export default function Marked() {
    
    // console.log(markedJobs)
    // console.log(typeof markedJobs)
    // const markedJobsArray = Object.values(markedJobs)
    // console.log(markedJobsArray)
    const [markedJobs, setMarkedJobs] = useContext(MarkedContext)
    return markedJobs.map((job =>  <Job job={job} />))
    
}
