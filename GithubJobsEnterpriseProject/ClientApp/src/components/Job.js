import React, { useState, useContext, useEffect } from 'react';
import { MarkedContext } from '../MarkedContext';
import EmptyHeart from '../empty-heart.png';
import FullHeart from '../full-heart.png';
import './Job.css';

export default function Job(props) {
    const [markedJobs, setMarkedJob] = useContext(MarkedContext);
    const [showedPicture, setShowedPicture] = useState([]);

    useEffect(() => {
        setDefaultPicture();
    }, [])



    const setDefaultPicture = () => {
        let pictureToSet = (props.job.is_marked) ? FullHeart : EmptyHeart
        setShowedPicture(pictureToSet)
        }

    const handleClickJob = (e) => {
        let pictureToSet = (props.job.is_marked) ? EmptyHeart : FullHeart
        console.log(props.job.is_marked)
        props.job.is_marked = (props.job.is_marked) ? false : true
        console.log(props.job.is_marked)
        setShowedPicture(pictureToSet)
        if (props.job.is_marked == true) {
            setMarkedJob(jobs => [...jobs, props.job])
        } else {
            const newList = markedJobs.filter((item) => item.id !== props.job.id);

            setMarkedJob(newList)
        }
        

    }

    const cardStyling = {
        width: "350px",
        height: "215px",
        display: "inline-block",
        borderStyle: "solid",
        borderRadius: "30px",
        margin: "20px",
        padding: "5px"
    }

    const heartStyling = {
        height: "40px",
        display: "inline-block"
    }

    let link = '/detail?id=' + props.job.id;

    return (
        <div className="jobCard" style={cardStyling}>
            <div style={{ height: "80px" }}><a href={link} style={{ fontSize: '20px' }}>{props.job.title}</a>
                <img src={showedPicture} style={heartStyling} alt="Empty heart" onClick={handleClickJob} /></div>
            <div style={{ height: "45px" }}><p>{props.job.location}</p></div>
            <div style={{ height: "45px" }}><p><strong>Company:</strong> {props.job.company}</p></div>
            <div style={{ height: "30px" }}><p><strong>Type:</strong> {props.job.type}</p></div>
        </div>
    )
}