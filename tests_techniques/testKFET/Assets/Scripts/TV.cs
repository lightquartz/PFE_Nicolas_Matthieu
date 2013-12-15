using UnityEngine;
using System.Collections;

public class TV : Interactive
{
    private const string INTERACTIVE_MESSAGE = "Click To Change Channel";
    public int currTVChannel = 1;
    public Material[] tvChannelMaterials;

    private MeshRenderer tvScreenMesh;
    private GameObject tvDial; 

    public void Awake()
    {
        tvScreenMesh = this.transform.FindChild("TVScreen").GetComponent<MeshRenderer>(); //get tv screen mesh
        tvDial = this.transform.FindChild("RetroTV").FindChild("Dial").gameObject; //get tv dial
        checkAvailableTVChannel();
        updateTVChannel();
    }

    public override string GetInteractionMessage()
    {
        return INTERACTIVE_MESSAGE;
    }

    public override void Interact()
    {
        nextTVChannel();
    }

    //tests to make sure desired channel is not out of bounds
    private void checkAvailableTVChannel()
    {
        if (currTVChannel<0)
        {
           currTVChannel=0;
        }
        else if (currTVChannel >= tvChannelMaterials.Length)
        {
            currTVChannel = tvChannelMaterials.Length - 1;
        }
    }

    private void nextTVChannel()
    {
        currTVChannel++;
        if (currTVChannel >= tvChannelMaterials.Length)
        {
            currTVChannel = 0;
        }
        changeChannel();
    }

    private void previousTVChannel()
    {
        currTVChannel--;
        if (currTVChannel < 0)
        {
            currTVChannel = tvChannelMaterials.Length - 1;
        }
        changeChannel();
    }

    private void changeChannel()
    {
        updateTVChannel();
        updateDialRotation();
    }

    private void updateTVChannel()
    {
        tvScreenMesh.material = tvChannelMaterials[currTVChannel];   
    }

    private void updateDialRotation()
    {
        tvDial.transform.Rotate(Vector3.forward,(360 / tvChannelMaterials.Length)); 
    }
}