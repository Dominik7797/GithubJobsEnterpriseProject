import React, { useContext } from 'react';
import Job from './Job';
import {MarkedContext} from '../../MarkedContext';

export default function Marked() {
    const [markedJobs] = useContext(MarkedContext)
    return markedJobs.map((job =>  <Job job={job} />))
    
}
