import LinearProgress from '@mui/material/LinearProgress';

const RatingProgressBar = (props) => {
    var color = ""
    if(props.value < 50)
        color = "4, 63%, 45%"
    else if(props.value < 80)
        color = "59, 100%, 40%"
    else 
        color = "144, 100%, 25%"

    return <div className='ratingProgressContainer'>
        <div className='flex flex-ai-c flex-jc-sb ratingProgressContainerTxt'>
            <h4 className='ratingType'>{props.ratingType}</h4>
            <h4 className='value'>{props.value}</h4>
        </div>
        <LinearProgress sx={{
    backgroundColor: `hsl(${color},0.25)`,
    "& .MuiLinearProgress-bar": {
      backgroundColor: `hsl(${color})`
    }
  }} variant="determinate" value={props.value} />
    </div>
}

export default RatingProgressBar;