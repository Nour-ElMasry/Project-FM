import LinearProgress from '@mui/material/LinearProgress';

const RatingProgressBar = (props) => {
    var color = ""
    if(props.value < 50)
        color = "error"
    else if(props.value < 80)
        color = "warning"
    else 
        color = "success"

    return <div className='ratingProgressContainer'>
        <div className='flex flex-ai-c flex-jc-sb ratingProgressContainerTxt'>
            <h4 className='ratingType'>{props.ratingType}</h4>
            <h4 className='value'>{props.value}</h4>
        </div>
        <LinearProgress color={color} variant="determinate" value={props.value} />
    </div>
}

export default RatingProgressBar;