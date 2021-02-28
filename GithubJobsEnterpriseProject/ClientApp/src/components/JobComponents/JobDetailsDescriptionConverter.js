import React from 'react'

export default function DetailsDescriptionConverter(DetailDesc) {
        let parser = new DOMParser();
        let desc = parser.parseFromString("<div>" + DetailDesc.DetailDesc + "</div>", 'text/html');
    
    return (
        <div>
            {desc.body.getElementsByTagName('div')[0].innerText}
        </div>
    );
}
