using System;
using Ventuz.Kernel;
using Phidgets;
using Phidgets.Events;


public class Script : ScriptBase, System.IDisposable
{
    
    // This member is used by the Validate() method to indicate
    // whether the Generate() method should return true or false
    // during its next execution.
    private bool changed;
	static Encoder encoder;
	
    // This Method is called if the component is loaded/created.
    public Script()
    {
        // Note: Accessing input or output properties from this method
        // will have no effect as they have not been allocated yet.
	        
			encoder = new Phidgets.Encoder();
			
			encoder.PositionChange += new EncoderPositionChangeEventHandler( encoder_PositionChange );
			
			encoder.open();

			encoder.waitForAttachment();

		
    }
    
    // This Method is called if the component is unloaded/disposed
    public virtual void Dispose()
    {
		
		encoder.close();
		
		encoder = null;
    }
    
    // This Method is called if an input property has changed its value
    public override void Validate()
    {
        // Remember: set changed to true if any of the output 
        // properties has been changed, see Generate()
    }
    
    // This Method is called every time before a frame is rendered.
    // Return value: if true, Ventuz will notify all nodes bound to this
    //               script node that one of the script's outputs has a
    //               new value and they therefore need to validate. For
    //               performance reasons, only return true if output
    //               values really have been changed.
    public override bool Generate()
    {
        if (changed)
        {
            changed = false;
            return true;
        }

        return false;
    }
	
	public void encoder_PositionChange(object sender, EncoderPositionChangeEventArgs e)
	{
		
		position = encoder.encoders[e.Index];
		changed = true;
	
	}
}
