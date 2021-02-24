import React, { useState, useEffect } from 'react'
import Job from './Job';

export default function Home(props) {

    const handleSubmit = (job) => {
        props.onChange(job)
    }


    function showElements(job) {
        
        return (
                <Job job={job} onChange={handleSubmit} />
        )
    }

    return props.jobs.map(showElements)
}
