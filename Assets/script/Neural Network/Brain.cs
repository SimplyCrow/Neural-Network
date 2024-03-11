using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Brain
{
    private Layer[] layers;

    public Brain(int[] shape) 
    {
        layers = new Layer[shape.Length - 1];

        for (int i = 0; i < layers.Length; i++)
        {
            layers[i] = new Layer(shape[i], shape[i + 1]);
            layers[i].RandomWeights(-2f, 2f);
        }
    }

    public float[] Calculate(float[] inputs)
    {
        for (int i = 0; i < layers.Length; i++)
        {
            if (i == 0)
            {
                layers[i].Forward(inputs);
                layers[i].Activision();
            }
            else if (i == layers.Length - 1)
            {
                layers[i].Forward(layers[i - 1].nodeValues);
            }
            else
            {
                layers[i].Forward(layers[i - 1].nodeValues);
                layers[i].Activision();
            }
        }
        return layers[layers.Length - 1].nodeValues;
    }

    public void Mutate()
    {
        for (int i = 0; i < layers.Length; i++)
        {
            if (i != 0 && i != layers.Length - 1)
            {
                layers[i].Mutate(0.5f);
            }
        }
    }

    public void SetLayers(Layer[] layers)
    {
        this.layers = layers;
    }

    public Layer[] GetLayers()
    {
        return layers;
    }

}

public class Layer
{
    public float[,] weights;
    public float[] biases;
    public float[] nodeValues;
    public bool isTanH = true;

    private int nodesCount;
    private int nodesInputCount;

    public Layer(int inputCount, int nodesCount)
    {
        this.nodesCount = nodesCount;
        nodesInputCount = inputCount;
        weights = new float[nodesCount, inputCount];
        biases = new float[nodesCount];
        nodeValues = new float[nodesCount];
    }

    public Layer(int inputCount, int nodesCount, bool isTanH)
    {
        this.nodesCount = nodesCount;
        this.nodesInputCount = inputCount;
        weights = new float[nodesCount, inputCount];
        biases = new float[nodesCount];
        nodeValues = new float[nodesCount];

        this.isTanH = isTanH;
    }

    public void Mutate(float mutateRate)
    {
        if (UnityEngine.Random.value < mutateRate)
        {
            for (int i = 0; i < UnityEngine.Random.Range(1, 4); i++)
            {
                weights[UnityEngine.Random.Range(0, weights.GetLength(0)), UnityEngine.Random.Range(0, nodesInputCount - 1)] += UnityEngine.Random.Range(-0.01f, 0.01f);
            }
            CircleGenrator.mutCount++;
        }
        
        if (UnityEngine.Random.value < mutateRate)
        {
            for (int i = 0; i < UnityEngine.Random.Range(1, 4); i++)
            {
                biases[UnityEngine.Random.Range(0, nodesCount)] += UnityEngine.Random.Range(-0.01f, 0.01f);
            }
            CircleGenrator.mutCount++;
        }
    }

    public void RandomWeights(float min = 0, float max = 1)
    {
        for (int i = 0; i < nodesCount; i++)
        {
            for (int j = 0; j < nodesInputCount; j++)
            {
                weights[i, j] = UnityEngine.Random.Range(min, max);
            }
        }
    }

    public void Forward(float[] input)
    {
        for (int i = 0; i < nodesCount; i++)
        {
            float sum = 0;
            for (int j = 0; j < nodesInputCount; j++)
            {
                sum += weights[i, j] * input[j];
            }
            sum += biases[i];
            nodeValues[i] = sum;
        }
    }

    public void Activision()
    {

        for (int i = 0; i < nodesCount; i++)
        {
            if (!isTanH)
            {
                if (nodeValues[i] < 0)
                    nodeValues[i] = 0;
            }
            else
            {
                nodeValues[i] = (float)Math.Tanh(nodeValues[i]);
            }
        }
    }

    public void Activision(bool withTanH)
    {

        for (int i = 0; i < nodesCount; i++)
        {
            if (!withTanH)
            {
                if (nodeValues[i] < 0)
                    nodeValues[i] = 0;
            }
            else
            {
                nodeValues[i] = (float)Math.Tanh(nodeValues[i]);
            }
        }
    }

}
